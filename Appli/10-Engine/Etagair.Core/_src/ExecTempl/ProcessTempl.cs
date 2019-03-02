using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// Execute template, create entity, folder,... object from a template.
    /// </summary>
    public class ProcessTempl
    {
        IEtagairReposit _reposit;

        /// <summary>
        /// process the entity properties from the template.
        /// </summary>
        ProcessEntPropTempl _processEntPropTempl;

        public ProcessTempl(IEtagairReposit reposit)
        {
            _reposit = reposit;
            // _execPropTemplRules = new ExecPropTemplRules(_reposit);
            _processEntPropTempl = new ProcessEntPropTempl(_reposit);
        }

        #region Public create entity
        /// <summary>
        /// Start create/instanciate an entity from a template, under the root folder.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <returns></returns>
        public EntityTemplToInst CreateEntity(EntityTempl entityTempl)
        {
            //List<PropTemplRuleActionBase> listAction = new List<PropTemplRuleActionBase>();
            return CreateEntity(entityTempl, _reposit.Finder.GetRootFolder()); //,listAction);
        }


        /// <summary>
        /// Start create/instanciate an entity from a template, under a folder parent.
        /// return the list of rules. (each rule need an action).
        /// Next step is to call the AddActionsToCreateEntity method to add actions or directly CreateEntity if there is no action.
        /// </summary>
        /// <param name="entityTempl"></param>
        /// <param name="folderParent"></param>
        /// <returns></returns>
        public EntityTemplToInst CreateEntity(EntityTempl entityTempl, Folder folderParent)
        {
            // parent folder is null? its the root
            if (folderParent == null)
                folderParent = _reposit.Finder.GetRootFolder();

            // create the object managing the process
            EntityTemplToInst templToInst = new EntityTemplToInst();
            templToInst.EntityTempl = entityTempl;
            templToInst.FolderParent = folderParent;

            // check the consistency and get rule to execute (need an action) of the entity template
            List<PropTemplRuleBase> listRuleToExec = new List<PropTemplRuleBase>();
            if (_processEntPropTempl.CheckConsistencyEntityTempl(entityTempl, listRuleToExec) > 0)
            {
                templToInst.SetNextStep(TemplToInstStep.Ends);
                templToInst.SetState(TemplToInstState.Failed);

                templToInst.SetNewListRule(listRuleToExec);

                // set the problem type: Consistency 
                // TODO: save the number of problems! (and details?)

                return templToInst;
            }

            // ok, template is consistent, no rule, can create the entity, doesn't need action
            if(listRuleToExec.Count==0)
            {
                templToInst.SetNextStep(TemplToInstStep.Starts);
                templToInst.SetState(TemplToInstState.InProgress);

                // can complete the creation of the entity
                CompleteCreateEntity(templToInst);

                return templToInst;
            }

            // ok, template is consistent, rules exists, need all actions before creates the entity
            templToInst.SetNextStep(TemplToInstStep.NeedAction);
            templToInst.SetState(TemplToInstState.InProgress);
            templToInst.SetNewListRule(listRuleToExec);
            return templToInst;
        }

        public bool AddActionsToCreateEntity(EntityTemplToInst templToInst, PropTemplRuleActionBase action)
        {
            List<PropTemplRuleActionBase> listAction = new List<PropTemplRuleActionBase>();
            listAction.Add(action);
            return AddActionsToCreateEntity(templToInst, listAction);
        }

        /// <summary>
        /// Add list of actions matching rules to create the entity from the template.
        /// </summary>
        /// <param name="templToInst"></param>
        /// <param name="listAction"></param>
        /// <returns></returns>
        public bool AddActionsToCreateEntity(EntityTemplToInst templToInst, List<PropTemplRuleActionBase> listAction)
        {
            // match actions to rules to execute
            List<PropTemplRuleBase> listRuleToExecWithoutAction;
            List<PropTemplRuleActionBase> listActionMatches;
            bool res = MatchActionsToRules(templToInst.ListRule, listAction, out listRuleToExecWithoutAction, out listActionMatches);

            // save the actions match the rules and rules without action
            templToInst.AddActions(listActionMatches);
            templToInst.AddRulesWithoutAction(listRuleToExecWithoutAction);

            // actions don't match rules? or rules don't have actions?
            if (!res)
            {
                // one or several rules don't have an action!
                templToInst.SetNextStep(TemplToInstStep.NeedAction);
                templToInst.SetState(TemplToInstState.InProgress);
                return false;
            }

            // ok, each rule have an action, can continue create the entity
            templToInst.SetNextStep(TemplToInstStep.Starts);
            templToInst.SetState(TemplToInstState.InProgress);
            return true;
        }

        /// <summary>
        /// Create an entity from a template.
        /// Before, need to call: StartCreateEntity and AddActionsToCreateEntity (if need actions).
        /// </summary>
        /// <param name="templToInst"></param>
        /// <returns></returns>
        public bool CompleteCreateEntity(EntityTemplToInst templToInst)
        {
            // check
            if (templToInst.NextStep != TemplToInstStep.Starts)
                return false;

            if(templToInst.State != TemplToInstState.InProgress)
                return false;

            // now create the entity, attach it under the parent
            Entity entity = new Entity();
            entity.ParentFolderId = templToInst.FolderParent.Id;
            entity.SetCreatedFromTempl(templToInst.EntityTempl);
            templToInst.Entity = entity;

            // set a key to the property root, from the template

            _processEntPropTempl.SetEntPropertyRootFromTempl(templToInst.EntityTempl, entity);

            if (!_reposit.Builder.SaveEntity(entity))
                return false;

            // build properties of the entity, step by step, from the property root
            List<PropTemplRuleBase> listRuleWithoutAction;
            if (!_processEntPropTempl.CreateAllPropGroupChildsFromTempl(templToInst.EntityTempl, templToInst.EntityTempl.PropertyRoot, entity, entity.PropertyRoot, templToInst.ListActionMatches, out listRuleWithoutAction))
            {
                templToInst.SetNextStep(TemplToInstStep.Ends);
                templToInst.SetState(TemplToInstState.Failed);

                // set the problem type: error occurs on instantiating properties
                // TODO:
                return false;
            }

            // no rule to execute, the creation of the entity is incomplete
            if (listRuleWithoutAction.Count > 0)
            {
                // TODO: can't be possible!! after the refactoring; detected at the start
                // the creation of the entity needs some actions on rules
                templToInst.SetNextStep(TemplToInstStep.Ends);
                templToInst.SetState(TemplToInstState.Failed);
                // save rules
                //templToInst.SetNewListRule(listRuleWithoutAction);
                return false;
            }

            // no rule to execute, the creation of the entity is complete
            templToInst.Entity.BuildFinished = true;
            templToInst.SetNextStep(TemplToInstStep.Ends);
            templToInst.SetState(TemplToInstState.Success);

            // save it
            if (!_reposit.Builder.UpdateEntity(entity))
                return false;

            templToInst.FolderParent.AddChild(entity);

            // update the parent folder, has a new child
            if (!_reposit.Builder.UpdateFolder(templToInst.FolderParent))
                return false;

            return true;
        }


        /// <summary>
        /// Continue the execution of the entity.
        /// Pendind rules should be feed with matching actions.
        /// 
        /// TODO: non, n'est plus utile!!
        /// </summary>
        /// <param name="entityTemplToInst"></param>
        /// <returns></returns>
        //public bool ContinueCreateEntity(EntityTemplToInst templToInst)
        //{
        //    // really need to continue?
        //    if (templToInst.NextStep != TemplToInstStep.NeedAction)
        //        return true;

        //    if(templToInst.State != TemplToInstState.InProgress)
        //        return true;

        //    // TODO: non, plus utile!!
        //    templToInst.SetNextStep(TemplToInstStep.Ends);
        //    templToInst.SetState(TemplToInstState.Failed);
        //    return false;
        //}

        #endregion


        #region Private match action-rule methods

        /// <summary>
        /// Match actions to rules to execute.
        /// </summary>
        /// <param name="templToInst"></param>
        /// <param name="listRuleToExec"></param>
        /// <param name="listAction"></param>
        /// <returns></returns>
        private bool MatchActionsToRules(List<PropTemplRuleBase> listRuleToExec, List<PropTemplRuleActionBase> listAction, out List<PropTemplRuleBase> listRuleToExecWithoutAction, out List<PropTemplRuleActionBase> listActionMatches)
        {
            listRuleToExecWithoutAction = new List<PropTemplRuleBase>();
            listActionMatches = new List<PropTemplRuleActionBase>();

            // scan each rule, must have a corresponding action
            foreach (PropTemplRuleBase rule in listRuleToExec)
            {
                // find the action
                PropTemplRuleActionBase action = listAction.Find(a => a.RuleId.Equals(rule.Id));
                if (action == null)
                    listRuleToExecWithoutAction.Add(rule);
                else
                    listActionMatches.Add(action);
            }

            if (listRuleToExecWithoutAction.Count > 0)
                return false;

            return true;
        }

        #endregion


    }
}
