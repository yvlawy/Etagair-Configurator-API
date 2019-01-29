using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.Contract
{
    public interface IFinder
    {
        //IEnumerable<ICatalog> GetListCatalog();

        //ICatalog FindCatalogByName(string name);

        /// <summary>
        /// Return the unique catalog saved inthe repos.
        /// </summary>
        ICatalog GetCatalog();

        /// <summary>
        /// Find the language in the list of managed languages.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        LanguageDef FindLanguageDefByCode(LanguageCode code);

        string GetCurrLanguageId();

        Language FindLanguageById(string languageId);            

        /// <summary>
        /// Find the language in the list of managed languages.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Language FindLanguageByCode(LanguageCode code);


        //TextLocalMain FindTextLocalMain(MainLanguageCode mainLanguageCode, string textCodeId);

        TextCode FindTextCodeById(string id);

        /// <summary>
        /// Find a textCode by the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        TextCode FindTextCodeByCode(string code);

        TextLocalModel FindTextLocal(LanguageCode languageCode, string textCodeId);

        Folder GetRootFolder();
        Folder FindFolderById(string folderId);

        Folder FindFolder(Folder folderParent, string name, bool inChilds);

        Entity FindEntityById(string entityId);

    }
}
