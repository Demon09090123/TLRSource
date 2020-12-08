package kabam.rotmg.account.web.commands
{
   import flash.display.Sprite;
   import kabam.rotmg.account.core.Account;
   import kabam.rotmg.core.model.ScreenModel;
   import kabam.rotmg.core.signals.InvalidateDataSignal;
   import kabam.rotmg.core.signals.SetScreenWithValidDataSignal;
import kabam.rotmg.ui.characterView.CharacterSelectionAndNewsScreen;

public class WebLogoutCommand
   {
       
      
      [Inject]
      public var account:Account;
      
      [Inject]
      public var invalidate:InvalidateDataSignal;
      
      [Inject]
      public var setScreenWithValidData:SetScreenWithValidDataSignal;
      
      [Inject]
      public var model:ScreenModel;
      
      public function WebLogoutCommand()
      {
         super();
      }
      
      public function execute() : void
      {
         this.account.clear();
         this.invalidate.dispatch();
         this.setScreenWithValidData.dispatch(this.makeScreen());
      }
      
      private function makeScreen() : Sprite
      {
         return new (this.model.currentType || CharacterSelectionAndNewsScreen)();
      }
   }
}
