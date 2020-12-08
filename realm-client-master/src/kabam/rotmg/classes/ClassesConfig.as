package kabam.rotmg.classes
{
   import kabam.rotmg.account.core.signals.CharListDataSignal;
   import kabam.rotmg.account.core.signals.LogoutSignal;
   import kabam.rotmg.assets.EmbeddedData;
   import kabam.rotmg.classes.control.ParseCharListXmlCommand;
   import kabam.rotmg.classes.control.ParseClassesXMLSignal;
   import kabam.rotmg.classes.control.ParseClassesXmlCommand;
   import kabam.rotmg.classes.control.ParseSkinsXmlCommand;
   import kabam.rotmg.classes.control.ResetClassDataCommand;
   import kabam.rotmg.classes.model.ClassesModel;
   import kabam.rotmg.classes.view.CharacterSkinListItem;
   import kabam.rotmg.classes.view.CharacterSkinListItemFactory;
   import kabam.rotmg.classes.view.CharacterSkinListItemMediator;
   import kabam.rotmg.classes.view.ClassDetailMediator;
   import kabam.rotmg.classes.view.ClassDetailView;
   import org.swiftsuspenders.Injector;
   import robotlegs.bender.extensions.mediatorMap.api.IMediatorMap;
   import robotlegs.bender.extensions.signalCommandMap.api.ISignalCommandMap;
   import robotlegs.bender.framework.api.IConfig;
   import robotlegs.bender.framework.api.IContext;
   
   public class ClassesConfig implements IConfig
   {
       
      
      [Inject]
      public var context:IContext;
      
      [Inject]
      public var injector:Injector;
      
      [Inject]
      public var mediatorMap:IMediatorMap;
      
      [Inject]
      public var commandMap:ISignalCommandMap;
      
      public function ClassesConfig()
      {
         super();
      }
      
      public function configure() : void
      {
         this.injector.map(ClassesModel).asSingleton();
         this.injector.map(CharacterSkinListItemFactory).asSingleton();
         this.mediatorMap.map(CharacterSkinListItem).toMediator(CharacterSkinListItemMediator);
         this.mediatorMap.map(ClassDetailView).toMediator(ClassDetailMediator);
         this.commandMap.map(LogoutSignal).toCommand(ResetClassDataCommand);
         this.commandMap.map(CharListDataSignal).toCommand(ParseCharListXmlCommand);
         this.commandMap.map(ParseClassesXMLSignal).toCommand(ParseClassesXmlCommand);
         this.commandMap.map(ParseClassesXMLSignal).toCommand(ParseSkinsXmlCommand);
         this.context.lifecycle.afterInitializing(this.init);
      }
      
      private function init() : void
      {
         var xml:XML = XML(new EmbeddedData.PlayersCXML());
         var parseClasses:ParseClassesXMLSignal = this.injector.getInstance(ParseClassesXMLSignal);
         parseClasses.dispatch(xml);
      }
   }
}
