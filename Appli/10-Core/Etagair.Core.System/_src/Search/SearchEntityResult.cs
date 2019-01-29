using System.Collections.Generic;

namespace Etagair.Core.System
{
    /// <summary>
    /// Search entities result.
    /// Save the list of id of selected entities.
    /// </summary>
    public class SearchEntityResult
    {
        public SearchEntityResult()
        {
            ListEntityId = new List<string>();
        }

        /// <summary>
        /// the search entity definition.
        /// </summary>
        public string SearchEntityId { get; set; }

        //private IEtagairReposit _reposit;

        //private List<ISearchEntityUnitResult> _listSearchResults = new List<ISearchEntityUnitResult>();

        //private ISearchEntityUnitResult _lastSearchResult = null;

        //private Entity _currEntity = null;

        /// <summary>
        /// The result: list of selected/found entities.
        /// </summary>
        public List<string> ListEntityId { get; private set; }

        /// <summary>
        /// To get all entities, there are saved in the repository
        /// </summary>
        //public void SetReposit(IEtagairReposit reposit)
        //{
        //    _reposit = reposit;
        //}

        public void AddEntity(Entity entity)
        {
            // check that the entity is not already added
            // todo:

            ListEntityId.Add(entity.Id);
        }

        //public void AddSearchResult(ISearchEntityUnitResult searchResult)
        //{
        //    _listSearchResults.Add(searchResult);
        //    _lastSearchResult = searchResult;
        //}

        
        /// <summary>
        /// Get the first entity of the last result.
        /// </summary>
        /// <returns></returns>
        //public Entity GetCurrent()
        //{
        //    if (_lastSearchResult == null)
        //        return null;

        //    //_currEntity = _lastSearchResult.GetFirst();
        //    //return _currEntity;
        //    return _lastSearchResult.GetCurrent();
        //}

        //public bool MoveNext()
        //{
        //    if (_lastSearchResult == null)
        //        return false;

        //    return _lastSearchResult.MoveNext();
        //}
    }
}
