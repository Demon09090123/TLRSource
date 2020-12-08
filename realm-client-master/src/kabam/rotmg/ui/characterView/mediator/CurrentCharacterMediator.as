package kabam.rotmg.ui.characterView.mediator {
import com.company.assembleegameclient.appengine.SavedCharacter;

import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.classes.model.ClassesModel;

import kabam.rotmg.core.model.PlayerModel;

import kabam.rotmg.core.signals.SetScreenSignal;
import kabam.rotmg.game.model.GameInitData;
import kabam.rotmg.game.signals.PlayGameSignal;
import kabam.rotmg.ui.characterView.CurrentCharacterScreen;
import kabam.rotmg.ui.welcomeview.WelcomeScreen;

import robotlegs.bender.bundles.mvcs.Mediator;

public class CurrentCharacterMediator extends Mediator {

    [Inject]
    public var view:CurrentCharacterScreen;
    [Inject]
    public var playGame:PlayGameSignal;
    [Inject]
    public var setScreen:SetScreenSignal;
    [Inject]
    public var playerModel:PlayerModel;
    [Inject]
    public var classesModel:ClassesModel;

    public function CurrentCharacterMediator() {
        super();
    }

    override public function initialize() : void
    {
        this.view.onPlay.add(onPlay);
        this.view.onClose.add(onClose);
    }

    private function onClose():void {
        this.setScreen.dispatch(new WelcomeScreen());
    }

    private function onPlay():void {
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
