import { expect } from '@playwright/test';
import { BasePage } from './base-page';
import { Element } from '../core/element/element';
import { BASE_URL } from '../constant/url';

export class MyProfilePage extends BasePage {
    private readonly editProfileButton: Element;
    private readonly profileFullName: Element;
    private readonly collectionsTab: Element;

    constructor() {
        super();
        this.editProfileButton = new Element("//a[text()='Edit profile']");
        this.profileFullName = new Element("//div[contains(@class,'name') and contains(@class,'responsiveHeading')]");
        this.collectionsTab = new Element("//a[@data-testid='user-nav-link-collections']");
    }

    async openMyProfilePage(username: string): Promise<void> {
        await this.openUrl(`${BASE_URL}/@${username}`);
    }

    async clickEditProfileButton(): Promise<void> {
        await this.editProfileButton.clickOnElement();
        await this.waitForPageLoad();
    }

    async clickCollectionsTab(): Promise<void> {
        await this.collectionsTab.clickOnElement();
        await this.waitForPageLoad();
    }

    async verifyProfilePageIsDisplayed(): Promise<void> {
        await this.profileFullName.shouldBeVisible();
        await expect(this.page).toHaveURL(/\/@/);
    }

    async verifyFullNameIsDisplayed(expectedFullName: string): Promise<void> {
        await this.profileFullName.doesElementContainText(expectedFullName);
    }
}