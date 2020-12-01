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
         return this.charList.credits_;
      }
      
      public function changeCredits(credits:int) : void
      {
         this.charList.credits_ = this.charList.credits_ + credits;
         this.creditsChanged.dispatch(this.charList.credits_);
      }
      
      public function setCredits(credits:int) : void
      {
         if(this.charList.credits_ != credits)
         {
            this.charList.credits_ = credits;
            this.creditsChanged.dispatch(credits);
         }
      }

      public function getAccountId() : int
      {
         return this.charList.accountId_;
      }
      
      public function hasAccount() : Boolean
      {
         return this.charList.accountId_ != -1;
      }

      public function getLevel() : int {
          if (this.charList.hasCharacter) {
              return this.charList.savedChar.level();
          }
          return 0;
      }

      
      public function getGuildName() : String
      {
         return this.charList.guildName_;
      }
      
      public function getGuildRank() : int
      {
         return this.charList.guildRank_;
      }
      public function getName() : String
      {
         return this.charList.name_;
      }
      
      public function setName(value:String) : void
      {
         this.charList.name_ = value;
      }

      public function getSavedCharacter() : SavedCharacter {
          if (this.charList.hasCharacter) {
              return this.charList.savedChar;
          }
          return null;
      }

      public function setCharacterList(savedCharactersList:SavedCharactersList) : void
      {
         this.charList = savedCharactersList;
      }
   }
}
