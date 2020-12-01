package kabam.rotmg.messaging.impl.incoming
{
   import flash.display.BitmapData;
   import flash.utils.IDataInput;
   
   public class Death extends IncomingMessage
   {
      public var accountId_:int;
      public var killedBy_:String;

      public function Death(id:uint, callback:Function)
      {
         super(id,callback);
      }
      
      override public function parseFromInput(data:IDataInput) : void
      {
         this.accountId_ = data.readInt();
         this.killedBy_ = data.readUTF();
      }
      
      override public function toString() : String
      {
         return formatToString("DEATH","accountId_","killedBy_");
      }
   }
}
