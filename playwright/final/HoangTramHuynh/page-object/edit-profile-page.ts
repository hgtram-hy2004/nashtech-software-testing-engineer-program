import { BasePage } from './base-page';
import { Element } from '../core/element/element';

export class EditProfilePage extends BasePage {
    private readonly usernameInput: Element;
    private readonly updateAccountButton: Element;
    private readonly updateAccountSuccessMessage: Element;

    constructor() {
        super();
        this.usernameInput = new Element('#user_username');
        this.updateAccountButton = new Element("//input[@type='submit' and @value='Update account']");
        this.updateAccountSuccessMessage = new Element("//div[normalize-space()='Account updated!']");
    }

    async editUsername(newUsername: string): Promise<void> {
        await this.usernameInput.shouldBeVisible();
        await this.usernameInput.clearText();
        await this.usernameInput.enterText(newUsername);
    }

    async clickUpdateAccountButton(): Promise<void> {
        await this.updateAccountButton.shouldBeVisible();
        await this.updateAccountButton.clickOnElement();
        await this.waitForPageLoad();
    }

    async verifyUpdateAccountSuccessMessageIsDisplayed(): Promise<void> {
        await this.updateAccountSuccessMessage.shouldBeVisible();
    }
}