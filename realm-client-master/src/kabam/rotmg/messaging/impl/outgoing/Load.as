package kabam.rotmg.messaging.impl.outgoing
{
   import flash.utils.IDataOutput;
   
   public class Load extends OutgoingMessage
   {
      public function Load(id:uint, callback:Function)
      {
         super(id,callback);
      }
      
      override public function writeToOutput(data:IDataOutput) : void
      {
      }
      
      override public function toString() : String
      {
         return formatToString("LOAD");
      }
   }
}
