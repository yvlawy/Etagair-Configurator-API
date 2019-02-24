using System;

namespace SamplesApp
{
    /// <summary>
    /// Contains code samples of the Etagair library.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Samples on Create entity and folder.
        /// </summary>
        static void SamplesCreateEntityFolder()
        {
            CreateEntityFolder createEntityFolder = new CreateEntityFolder();

            // create the engine and the database files
            //createEntityFolder.CreateEngine();

            // create a folder within an entity
            createEntityFolder.CreateFolderWithinEntity();

            createEntityFolder.CreateThreeEntitiesSearchPropKeyName();

            createEntityFolder.CreateEntityWithPropGroup();
        }

        /// <summary>
        /// Samples on managing language, text translation/localization.
        /// </summary>
        static void SamplesLanguageTranslation()
        {
            LanguageTranslation languageTranslation = new LanguageTranslation();


            languageTranslation.ManageLanguagesAndLocalizedText();

        }

        /// <summary>
        /// Samples on create template (entity for now)..
        /// </summary>
        static void SamplesTemplate()
        {
            EntityTemplate entityTemplate = new EntityTemplate();

            entityTemplate.CreateBasicEntityTemplate();
        }

        /// <summary>
        /// Execute a code sample.
        /// Comment/uncomment code.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("====Etagair code samples:");

            //SamplesCreateEntityFolder();

            //SamplesLanguageTranslation();

            // create template
            SamplesTemplate();

            Console.WriteLine("\nInput a key ...");
            Console.ReadKey();
        }
    }
}
