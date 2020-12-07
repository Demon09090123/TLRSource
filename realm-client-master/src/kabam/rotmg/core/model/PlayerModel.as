package kabam.rotmg.core.model
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
   import com.company.assembleegameclient.appengine.SavedCharactersList;
   import com.company.assembleegameclient.appengine.SavedNewsItem;
   import com.company.assembleegameclient.parameters.Parameters;
   import kabam.rotmg.account.core.Account;
   import org.osflash.signals.Signal;
   
   public class PlayerModel
   {
      public const creditsChanged:Signal = new Signal(int);
      public var charList:SavedCharactersList;
      public var isInvalidated:Boolean;
      
      [Inject]
      public var account:Account;
      
      public function PlayerModel()
      {
         super();
         this.isInvalidated = true;
      }

      public function getCredits() : int
      {
         return this.charList.getCredits();
      }

      public function getAccountId() : int
      {
         return this.charList.getAccountID();
      }
      
      public function hasAccount() : Boolean
      {
         return getAccountId() != -1;
      }

      public function getLevel() : int {
          if (hasCharacter()) {
              this.charList.getChar().level();
          }
          return 0;
      }

      
      public function getGuildName() : String
      {
         return this.charList.getGuildName();
      }
      
      public function getGuildRank() : int
      {
         return this.charList.getGuildRank();
      }
      public function getName() : String
      {
         return this.charList.getName();
      }
      public function getSavedCharacter() : SavedCharacter {
          return this.charList.getChar();
      }

      public function hasCharacter() : Boolean {
          return (this.charList.getChar() != null);
      }

      public function setCharacterList(savedCharactersList:SavedCharactersList) : void
      {
         this.charList = savedCharactersList;
      }
   }
}
