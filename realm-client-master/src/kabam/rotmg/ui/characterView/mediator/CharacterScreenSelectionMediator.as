package kabam.rotmg.ui.characterView.mediator {
import kabam.rotmg.ui.characterView.*;

import com.company.assembleegameclient.appengine.SavedCharacter;
import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.classes.model.ClassesModel;
import kabam.rotmg.core.model.PlayerModel;
import kabam.rotmg.core.signals.SetScreenSignal;
import kabam.rotmg.game.model.GameInitData;
import kabam.rotmg.game.signals.PlayGameSignal;

import robotlegs.bender.bundles.mvcs.Mediator;

public class CharacterScreenSelectionMediator extends Mediator
{


    [Inject]
    public var view:CharacterSelectionAndNewsScreen;

    [Inject]
    public var playerModel:PlayerModel;

    [Inject]
    public var classesModel:ClassesModel;

    [Inject]
    public var setScreen:SetScreenSignal;

    [Inject]
    public var playGame:PlayGameSignal;

    public function CharacterScreenSelectionMediator()
    {
        super();
    }

    override public function initialize() : void
    {
        this.view.initialize(this.playerModel);
    }

    private function onPlayGame() : void
    {
        var character:SavedCharacter = this.playerModel.getSavedCharacter();
        var characterClass:CharacterClass = this.classesModel.getCharacterClass(character.objectType());
        characterClass.setIsSelected(true);
        characterClass.skins.getSkin(character.skinType()).setIsSelected(true);
        var game:GameInitData = new GameInitData();
        game.createCharacter = false;
        game.isNewGame = true;
        this.playGame.dispatch(game);
    }
}
}
