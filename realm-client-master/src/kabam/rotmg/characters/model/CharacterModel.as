package kabam.rotmg.characters.model
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
   
   public interface CharacterModel
   {
      function getCharacter() : SavedCharacter;

      function select(param1:SavedCharacter) : void;
      
      function getSelected() : SavedCharacter;
   }
}
