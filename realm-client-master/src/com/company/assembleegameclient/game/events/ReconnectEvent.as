package com.company.assembleegameclient.game.events
{
   import flash.events.Event;
   import flash.utils.ByteArray;
   
   public class ReconnectEvent extends Event
   {
      public static const RECONNECT:String = "RECONNECT_EVENT";
      
      public var gameId_:int;
      
      public var createCharacter_:Boolean;
      
      public function ReconnectEvent(gameId:int, createCharacter:Boolean)
      {
         super(RECONNECT);
         this.gameId_ = gameId;
         this.createCharacter_ = createCharacter;
      }
      
      override public function clone() : Event
      {
         return new ReconnectEvent(this.gameId_,this.createCharacter_);
      }
      
      override public function toString() : String
      {
         return formatToString(RECONNECT,"server_","gameId_","charId_");
      }
   }
}
