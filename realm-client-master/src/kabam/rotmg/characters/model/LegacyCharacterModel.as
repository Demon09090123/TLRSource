package kabam.rotmg.characters.model
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
   import kabam.rotmg.core.model.PlayerModel;
   
   public class LegacyCharacterModel implements CharacterModel
   {
       
      
      [Inject]
      public var wrapped:PlayerModel;
      
      private var selected:SavedCharacter;
      
      public function LegacyCharacterModel()
      {
         super();
      }

      public function getCharacter() : SavedCharacter
      {
         return this.wrapped.getSavedCharacter();
      }

      public function select(character:SavedCharacter) : void
      {
         this.selected = character;
      }
      
      public function getSelected() : SavedCharacter
      {
         return this.selected;
      }
   }
}
