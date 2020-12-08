package kabam.rotmg.ui.commands
{
   import kabam.rotmg.account.core.Account;
   import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.core.signals.SetScreenWithValidDataSignal;
   import kabam.rotmg.dialogs.control.OpenDialogSignal;
   import kabam.rotmg.game.signals.PlayGameSignal;
   import kabam.rotmg.ui.characterView.CharacterSelectionAndNewsScreen;

public class EnterGameCommand
   {
      [Inject]
      public var account:Account;
      
      [Inject]
      public var model:PlayerModel;
      
      [Inject]
      public var setScreenWithValidData:SetScreenWithValidDataSignal;
      
      [Inject]
      public var playGame:PlayGameSignal;
      
      [Inject]
      public var openDialog:OpenDialogSignal;

      public function EnterGameCommand()
      {
         super();
      }
      
      public function execute() : void
      {
         this.showCurrentCharacterScreen();
      }
      
      private function showCurrentCharacterScreen() : void
      {
         this.setScreenWithValidData.dispatch(new CharacterSelectionAndNewsScreen());
      }
   }
}
