import { expect } from '@playwright/test';
import { BasePage } from './base-page';
import { BASE_URL } from '../constant/url';
import { Element } from '../core/element/element';

export class CollectionPage extends BasePage {

    private getCollectionCard(collectionName: string): Element {
        return new Element(`//div[contains(@class,'titleContainer')]//div[normalize-space()='${collectionName}']`);
    }

    private getCollectionPhotoImages(): Element {
        return new Element("//a[@itemprop='contentUrl']//img");
    }

    async openUserCollectionsPage(username: string): Promise<void> {
        await this.openUrl(`${BASE_URL}/@${username}/collections`);
        await this.waitForPageLoad();
    }

    async verifyCollectionCardIsDisplayed(collectionName: string): Promise<void> {
        await this.getCollectionCard(collectionName).shouldBeVisible();
    }

    async openCollection(collectionName: string): Promise<void> {
        await this.getCollectionCard(collectionName).shouldBeVisible();
        await this.getCollectionCard(collectionName).clickOnElement();
        await this.waitForPageLoad();
    }

    async verifyThreePhotosAppearInCollection(): Promise<void> {
        const photoImages = this.getCollectionPhotoImages().getElement();
        await expect(photoImages).toHaveCount(3, {timeout: 15000,});
    }
}