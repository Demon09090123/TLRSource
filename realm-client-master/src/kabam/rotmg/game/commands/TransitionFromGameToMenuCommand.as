package kabam.rotmg.game.commands
{
   import com.company.assembleegameclient.screens.CharacterSelectionAndNewsScreen;
   import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.core.signals.InvalidateDataSignal;
   import kabam.rotmg.core.signals.SetScreenSignal;
   import kabam.rotmg.core.signals.SetScreenWithValidDataSignal;
   
   public class TransitionFromGameToMenuCommand
   {
       
      
      [Inject]
      public var player:PlayerModel;

      [Inject]
      public var invalidate:InvalidateDataSignal;
      
      [Inject]
      public var setScreen:SetScreenSignal;
      
      [Inject]
      public var setScreenWithValidData:SetScreenWithValidDataSignal;

      public function TransitionFromGameToMenuCommand()
      {
         super();
      }
      
      public function execute() : void
      {
          this.invalidate.dispatch();
          this.showCurrentCharacterScreen();
      }

      private function showCurrentCharacterScreen() : void
      {
         this.setScreenWithValidData.dispatch(new CharacterSelectionAndNewsScreen());
      }
   }
}
