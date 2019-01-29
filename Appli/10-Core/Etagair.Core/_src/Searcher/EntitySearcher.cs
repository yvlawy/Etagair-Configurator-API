using Etagair.Core.Reposit.Contract;
using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core
{
    /// <summary>
    /// To search entity (process/manager).
    /// </summary>
    public class EntitySearcher
    {
        IEtagairReposit _reposit;

        public EntitySearcher(IEtagairReposit reposit)
        {
            _reposit = reposit;
        }

        /// <summary>
        /// Process, execute the search entity definition.
        /// Scan folder, one by one.
        /// </summary>
        public SearchEntityResult Process(SearchEntity searchEntity)
        {
            // create the result object
            SearchEntityResult result = new SearchEntityResult();

            // scan each source folder
            foreach (SearchEntitySrcFolder srcFolder in searchEntity.ListSearchEntitySrcFolder)
            {
                // get the folder object
                Folder folder = _reposit.Finder.FindFolderById(srcFolder.FolderId);

                // create a work/exec object of the criteria expression
                //SearchPropBaseWork searchPropBaseWork = CreateAllSearchPropBaseWork(searchEntity.SearchPropRoot);

                // search entities under the folder parent
                SearchInFolder(searchEntity, folder, srcFolder.GoInsideChilds, result);
            }

            // returns the final results
            return result;
        }

        #region Private methods

        /// <summary>
        /// Create all work objet matching object, recursivly.
        /// </summary>
        /// <param name="searchPropBase"></param>
        /// <returns></returns>
        private SearchPropBaseWork CreateAllSearchPropBaseWork(SearchPropBase searchPropBase)
        {
            // a bool expr, go inside left and right part
            SearchPropBoolExpr searchPropBoolExpr = searchPropBase as SearchPropBoolExpr;
            if (searchPropBoolExpr != null)
            {
                SearchPropBoolExprWork searchPropBoolExprWork = new SearchPropBoolExprWork();
                searchPropBoolExprWork.SearchPropBaseId = searchPropBoolExpr.Id;
                searchPropBoolExprWork.Operator = searchPropBoolExpr.Operator;

                // create work object on the left side
                searchPropBoolExprWork.LeftOperand = CreateAllSearchPropBaseWork(searchPropBoolExpr.LeftOperand);

                // create work object on the right side
                searchPropBoolExprWork.RightOperand = CreateAllSearchPropBaseWork(searchPropBoolExpr.RightOperand);

                return searchPropBoolExprWork;
            }

            // a final criterion
            SearchPropCriterionKeyText propCritKeyText = searchPropBase as SearchPropCriterionKeyText;
            if(propCritKeyText!=null)
            {
                SearchPropCriterionKeyTextWork propCritKeyTextWork = new SearchPropCriterionKeyTextWork();
                propCritKeyTextWork.SearchPropBaseId = propCritKeyText.Id;
                propCritKeyTextWork.KeyText = propCritKeyText.KeyText;
                propCritKeyTextWork.PropChildsScan= propCritKeyText.PropChildsScan;
                propCritKeyTextWork.PropKeyTextType = propCritKeyText.PropKeyTextType;
                propCritKeyTextWork.TextMatch = propCritKeyText.TextMatch;
                propCritKeyTextWork.TextSensitive= propCritKeyText.TextSensitive;

                return propCritKeyTextWork;
            }
            // TODO: others final criterion type


            // not implemented
            throw new Exception("Search property not yet implemented!");
        }

        /// <summary>
        /// search entities under the folder parent.
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="folder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool SearchInFolder(SearchEntity searchEntity, Folder folder, bool goInsideChilds, SearchEntityResult result)
        {
            // process each entity in the folder source
            foreach(KeyValuePair<string, ObjectType> pair in folder.ListChildId)
            {
                // is it a folder?
                if(pair.Value == ObjectType.Folder)
                {
                    // go inside this folder child?
                    if(goInsideChilds)
                    {
                        // load the folder object
                        Folder folderChild = _reposit.Finder.FindFolderById(pair.Key);

                        SearchInFolder(searchEntity, folderChild, true, result);

                        // process next object
                        continue;
                    }
                }

                // is it an entity?
                if (pair.Value == ObjectType.Entity)
                {
                    // load the entity
                    Entity entity = _reposit.Finder.FindEntityById(pair.Key);

                    // analyze the criteria properties expression for the entity
                    AnalyzeEntityOnCriteria(searchEntity, entity, result);
                }

                // others cases: entity or folder template: do nothing
            }
            return true;
        }

        /// <summary>
        /// analyze the criteria properties expression for the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool AnalyzeEntityOnCriteria(SearchEntity searchEntity, Entity entity, SearchEntityResult result)
        {
            // create a work/exec object of the criteria expression
            SearchPropBaseWork searchPropBaseWork = CreateAllSearchPropBaseWork(searchEntity.SearchPropRoot);

            // a bool expr, go inside left and right part
            SearchPropBoolExprWork searchPropBoolExprWork = searchPropBaseWork as SearchPropBoolExprWork;
            if (searchPropBoolExprWork != null)
            {

                // create work object on the left side

                // create work object on the right side

                //return searchPropBoolExprWork;

                // ok, no error
                return true;
            }

            // a final criterion
            SearchPropCriterionKeyTextWork propCritKeyTextWork = searchPropBaseWork as SearchPropCriterionKeyTextWork;
            if (propCritKeyTextWork != null)
            {
                EntitySearcherPropKeyText searcherPropKeyText = new EntitySearcherPropKeyText(_reposit);
                return searcherPropKeyText.AnalyzeEntityOnCritKeyText(propCritKeyTextWork, entity, result);
            }

            // TODO: others final criterion type


            // not implemented
            throw new Exception("Search property not yet implemented!");
        }


        #endregion

    }
}
