using Etagair.Core.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.Reposit.Contract
{
    public interface IBuilder
    {
        bool SaveStructDescriptorRootFolderId(string id);

        bool SaveCatalog(ICatalog catalog);

        bool UpdateCatalog(ICatalog catalog);

        //bool DeleteCatalog(ICatalog catalog);


        // todo: pas utile! on retrouve par les LanguageDef
        //bool SaveMainLanguageCode(MainLanguageCode mainLangCode);

        bool SaveLanguageDef(LanguageDef langDef);

        bool SaveCurrLanguageId(string languageId);

        bool SaveLanguage(Language lang);

        bool SaveTextCode(TextCode tc);

        //bool SaveTextLocalMain(TextLocalMain textLocalMain);

        bool SaveTextLocal(TextLocalModel textLocal);

        //bool SaveRootFolder(Folder folder);

        bool SaveFolder(Folder folder);
        bool UpdateFolder(Folder folder);

        bool SaveEntity(Entity entity);
        bool UpdateEntity(Entity entity);

        bool SaveEntityTempl(EntityTempl entityTempl);
        bool UpdateEntityTempl(EntityTempl entityTempl);

    }
}
