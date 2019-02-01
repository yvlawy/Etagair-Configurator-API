using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// An instance under construction, from a template.
    /// EntityTemplToInst
    /// </summary>
    public abstract class TemplToInstBase
    {
        public TemplToInstBase()
        {
            NextStep = TemplToInstStep.Starts;
            State = TemplToInstState.InProgress;
        }

        /// <summary>
        /// the next step to execute: Starts, NeedAction, Ends.
        /// </summary>
        public TemplToInstStep NextStep { get; protected set; }

        /// <summary>
        /// The current state: InProgress, Success, Failed.
        /// </summary>
        public TemplToInstState State { get; protected set; }

        /// <summary>
        /// The folder parent where to set the instanciated object.
        /// </summary>
        public Folder FolderParent { get; set; }

        public void SetNextStep(TemplToInstStep step)
        {
            NextStep = step;
        }

        public void SetState(TemplToInstState state)
        {
            State = state;
        }

    }
}
