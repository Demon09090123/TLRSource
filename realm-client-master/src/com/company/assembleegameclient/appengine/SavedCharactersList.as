package com.company.assembleegameclient.appengine
{
   import flash.events.Event;
   import kabam.rotmg.account.core.Account;
   
   public class SavedCharactersList extends Event
   {
      public static const SAVED_CHARS_LIST:String = "SAVED_CHARS_LIST";

      private var origData:String;
      private var charsXML:XML;
      private var accountXML:XML;
      private var guildXML:XML;

      private var accountId:int;
      private var name:String;
      private var savedChar:SavedCharacter;
      private var credits:int = 0;
      private var guildName:String;
      private var guildRank:int;

      public function SavedCharactersList(data:String) {
          super (SAVED_CHARS_LIST);

          this.origData = data;
          this.charsXML = XML(this.origData);

          if (this.charsXML.hasOwnProperty("Account")) {
              parseUserData(XML(this.charsXML.Account));

              if (this.accountXML.hasOwnProperty("Guild")) {
                  parseGuildData(XML(this.accountXML.Guild));
              }
              if (this.charsXML.hasOwnProperty("Char")) {
                  parseCharacterData(XML(this.charsXML.Char))
              }
          }
      }

      private function parseUserData(accountXML:XML) : void
      {
         this.accountXML = accountXML;
         this.accountId = accountXML.AccountId;
         this.name = accountXML.Name;
         this.credits = int(accountXML.Stats.Credits);
      }

      private function parseGuildData(guildXML:XML) : void
      {
          this.guildXML = guildXML;
          this.guildName = guildXML.Name;
          this.guildRank = int(guildXML.Rank);
      }
      
      private function parseCharacterData(charXML:XML) : void
      {
          this.savedChar = new SavedCharacter(charXML);
      }

       public function getAccountID():int {
           return this.accountId;
       }
       public function getName():String {
           return this.name;
       }
       public function getCredits():int {
           return this.credits;
       }
       public function getChar():SavedCharacter {
           return this.savedChar;
       }
       public function getGuildName():String {
           return this.guildName;
       }
       public function getGuildRank():int {
           return this.guildRank;
       }

      override public function clone() : Event
      {
         return new SavedCharactersList(this.origData);
      }
      
      override public function toString() : String
      {
         return "";
      }
   }
}
