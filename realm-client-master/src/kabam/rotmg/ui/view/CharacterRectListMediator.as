package kabam.rotmg.ui.view
{
   import com.company.assembleegameclient.screens.NewCharacterScreen;
   import com.company.assembleegameclient.screens.charrects.CharacterRectList;
   import kabam.rotmg.core.signals.SetScreenWithValidDataSignal;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class CharacterRectListMediator extends Mediator
   {
       
      
      [Inject]
      public var view:CharacterRectList;
      
      [Inject]
      public var setScreenWithValidData:SetScreenWithValidDataSignal;

      public function CharacterRectListMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.view.newCharacter.add(this.onNewCharacter);
      }
      
      override public function destroy() : void
      {
         this.view.newCharacter.remove(this.onNewCharacter);
      }
      
      private function onNewCharacter() : void
      {
         this.setScreenWithValidData.dispatch(new NewCharacterScreen());
      }
   }
}
