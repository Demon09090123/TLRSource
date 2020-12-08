package kabam.rotmg.constants
{
   public class ItemConstants
   {
      
      public static const NO_ITEM:int = -1;
      
      public static const ALL_TYPE:int = 0;
      
      public static const SWORD_TYPE:int = 1;

      public static const BOW_TYPE:int = 2;

      public static const SHIELD_TYPE:int = 3;
      
      public static const LEATHER_TYPE:int = 4;
      
      public static const PLATE_TYPE:int = 5;

      public static const RING_TYPE:int = 6;

      public static const SPELL_TYPE:int = 7;

      public static const ROBE_TYPE:int = 8;

      public static const QUIVER_TYPE:int = 9;

      public static const STAFF_TYPE:int = 10;

      
      public function ItemConstants()
      {
         super();
      }
      
      public static function itemTypeToName(type:int) : String
      {
         switch(type)
         {
            case ALL_TYPE:
               return "Any";
            case SWORD_TYPE:
               return "Sword";
            case BOW_TYPE:
               return "Bow";
            case SHIELD_TYPE:
               return "Shield";
            case LEATHER_TYPE:
               return "Leather Armor";
            case PLATE_TYPE:
               return "Armor";
            case RING_TYPE:
               return "Accessory";
            case SPELL_TYPE:
               return "Spell";
            case ROBE_TYPE:
               return "Robe";
            case QUIVER_TYPE:
               return "Quiver";
            case STAFF_TYPE:
               return "Staff";
            default:
               return "Invalid Type!";
         }
      }
   }
}
