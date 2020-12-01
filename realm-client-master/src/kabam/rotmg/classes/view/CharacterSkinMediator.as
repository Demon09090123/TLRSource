package kabam.rotmg.classes.view
{
   import com.company.assembleegameclient.screens.NewCharacterScreen;
   import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.core.signals.SetScreenSignal;
   import kabam.rotmg.game.model.GameInitData;
   import kabam.rotmg.game.signals.PlayGameSignal;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class CharacterSkinMediator extends Mediator
   {
      [Inject]
      public var view:CharacterSkinView;
      
      [Inject]
      public var model:PlayerModel;
      
      [Inject]
      public var setScreen:SetScreenSignal;
      
      [Inject]
      public var play:PlayGameSignal;
      
      public function CharacterSkinMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
          var hasCharacter:Boolean = this.model.charList.hasCharacter;
          this.view.setPlayButtonEnabled(!hasCharacter);
          if (hasCharacter == false) {
              this.view.play.addOnce(onPlay);
          }

         this.view.back.addOnce(this.onBack);
      }
      
      override public function destroy() : void
      {
         this.view.back.remove(this.onBack);
         this.view.play.remove(this.onPlay);
      }
      
      private function onBack() : void
      {
         this.setScreen.dispatch(new NewCharacterScreen());
      }
      
      private function onPlay() : void
      {
         var game:GameInitData = new GameInitData();
         game.createCharacter = true;
         game.isNewGame = true;
         this.play.dispatch(game);
      }
   }
}
