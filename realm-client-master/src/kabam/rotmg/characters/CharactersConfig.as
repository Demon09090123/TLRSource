package kabam.rotmg.characters
{
   import kabam.rotmg.characters.model.CharacterModel;
   import kabam.rotmg.characters.model.LegacyCharacterModel;
   import org.swiftsuspenders.Injector;
   import robotlegs.bender.extensions.signalCommandMap.api.ISignalCommandMap;
   import robotlegs.bender.framework.api.IConfig;
   import robotlegs.bender.framework.api.IContext;
   
   public class CharactersConfig implements IConfig
   {
       
      
      [Inject]
      public var context:IContext;
      
      [Inject]
      public var injector:Injector;
      
      [Inject]
      public var commandMap:ISignalCommandMap;
      
      public function CharactersConfig()
      {
         super();
      }
      
      public function configure() : void
      {
         this.injector.map(CharacterModel).toSingleton(LegacyCharacterModel);
      }
   }
}
