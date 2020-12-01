package com.company.assembleegameclient.appengine
{
   import flash.events.Event;
   import kabam.rotmg.account.core.Account;
   
   public class SavedCharactersList extends Event
   {
      public static const SAVED_CHARS_LIST:String = "SAVED_CHARS_LIST";
      public static const AVAILABLE:String = "available";
      public static const UNAVAILABLE:String = "unavailable";
      public static const UNRESTRICTED:String = "unrestricted";

      private var origData_:String;
      private var charsXML_:XML;
      public var accountId_:int;
      public var savedChar:SavedCharacter;
      public var hasCharacter:Boolean = false;
      public var credits_:int = 0;
      public var guildName_:String;
      public var guildRank_:int;
      public var name_:String = null;
      private var account:Account;
      
      public function SavedCharactersList(data:String)
      {
         super(SAVED_CHARS_LIST);
         this.origData_ = data;
         this.charsXML_ = new XML(this.origData_);
         var accountXML:XML = XML(this.charsXML_.Account);
         this.hasCharacter = hasChar(XML(accountXML.HasCharacter));
         this.parseUserData(accountXML);
         this.parseGuildData(accountXML);
          if (hasCharacter == true) {
              this.parseCharacterData(this.charsXML_.Char[0]);
          }
      }

      private function hasChar(xml:XML):Boolean {
          return false;
      }

      private function parseUserData(accountXML:XML) : void
      {
         this.accountId_ = accountXML.AccountId;
         this.name_ = accountXML.Name;
         this.credits_ = int(accountXML.Stats.Credits);
      }

      private function parseGuildData(accountXML:XML) : void
      {
         var guildXML:XML = null;
         if(accountXML.hasOwnProperty("Guild"))
         {
            guildXML = XML(accountXML.Guild);
            this.guildName_ = guildXML.Name;
            this.guildRank_ = int(guildXML.Rank);
         }
      }
      
      private function parseCharacterData(charXML:XML) : void
      {
          this.savedChar = new SavedCharacter(charXML, this.name_);
      }

      override public function clone() : Event
      {
         return new SavedCharactersList(this.origData_);
      }
      
      override public function toString() : String
      {
         return "";
      }
   }
}
