package kabam.rotmg.game.commands
{
   import com.company.assembleegameclient.game.GameSprite;
   import com.company.assembleegameclient.parameters.Parameters;
   import flash.utils.ByteArray;
   import kabam.lib.tasks.TaskMonitor;
   import kabam.rotmg.account.core.services.GetCharListTask;
   import kabam.rotmg.core.model.PlayerModel;
   import kabam.rotmg.core.signals.SetScreenSignal;
   import kabam.rotmg.game.model.GameInitData;
   
   public class PlayGameCommand
   {
       
      
      [Inject]
      public var setScreen:SetScreenSignal;
      
      [Inject]
      public var data:GameInitData;
      
      [Inject]
      public var model:PlayerModel;
      
      [Inject]
      public var task:GetCharListTask;
      
      [Inject]
      public var monitor:TaskMonitor;
      
      public function PlayGameCommand()
      {
         super();
      }
      
      public function execute() : void
      {
         this.recordCharacterUseInSharedObject();
         this.makeGameView();
      }
      
      private function recordCharacterUseInSharedObject() : void
      {
         Parameters.data_.charIdUseMap = new Date().getTime();
         Parameters.save();
      }
      
      private function makeGameView() : void
      {
         var gameId:int = this.data.isNewGame ? int(this.getInitialGameId()) : int(this.data.gameId);
         var createCharacter:Boolean = this.data.createCharacter;
         this.setScreen.dispatch(new GameSprite(gameId,createCharacter,this.model,null));
      }
      
      private function getInitialGameId() : int
      {
         return Parameters.NEXUS_GAMEID;
      }
   }
}
