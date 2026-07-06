import { expect } from '@playwright/test';
import { LoginPage } from '../page-object/login-page';
import { HomePage } from '../page-object/home-page';
import { CollectionPage } from '../page-object/collection-page';
import { BrowserUtils } from '../core/browser/browser-utils';
import { BASE_URL } from '../constant/url';
import { AccountData } from '../data-object/ui/account';
import { PhotoResponse } from '../data-object/api/photo';
import { CreateCollectionResponse } from '../data-object/api/collection';
import { PhotoServices } from '../services/photo-services';
import { CollectionServices } from '../services/collection-services';

export class CollectionWorkflow {

    constructor(
        private readonly loginPage: LoginPage,
        private readonly homePage: HomePage,
        private readonly collectionPage: CollectionPage
    ) { }

    async login(accountInfo: AccountData): Promise<void> {
        await BrowserUtils.navigateTo(BASE_URL);
        await this.homePage.gotoLogin();
        await this.loginPage.login(accountInfo);
    }

    private isUnsplashPlusPhoto(photo: PhotoResponse): boolean {
        return photo.plus === true || photo.premium === true;
    }

    async createCollectionWithThreeRandomPhotos(collectionName: string): Promise<string> {

        const createCollectionResponse = await CollectionServices.createCollection(collectionName);
        const createCollectionBody = await createCollectionResponse.text();
        expect(createCollectionResponse.status(), `Create collection failed. Body: ${createCollectionBody}`).toBe(201);

        const collection = JSON.parse(createCollectionBody) as CreateCollectionResponse;
        const randomPhotosResponse = await PhotoServices.getRandomPhotos(20);
        const randomPhotosBody = await randomPhotosResponse.text();
        expect(randomPhotosResponse.status(), `Get random photos failed. Body: ${randomPhotosBody}`).toBe(200);

        const photos = JSON.parse(randomPhotosBody) as PhotoResponse[];
        let addedPhotoCount = 0;
        for (const photo of photos) {
            if (addedPhotoCount === 3) {
                break;
            }
            if (this.isUnsplashPlusPhoto(photo)) {
                console.log(`Skip Unsplash+ photo: ${photo.id}`);
                continue;
            }
            const addPhotoResponse = await CollectionServices.addPhotoToCollection(collection.id, photo.id);
            const addPhotoBody = await addPhotoResponse.text();
            expect(addPhotoResponse.status(), `Add photo ${photo.id} to collection failed. Body: ${addPhotoBody}`).toBe(201);
            addedPhotoCount++;
        }
        expect(addedPhotoCount, 'Not enough normal photos were added to collection. Unsplash+ photos are skipped.').toBe(3);
        return collection.id;
    }

    async goToUserCollectionsPage(username: string): Promise<void> {
        await this.collectionPage.openUserCollectionsPage(username);
    }

    async verifyCollectionImageCount(collectionName: string): Promise<void> {
        await this.collectionPage.verifyCollectionCardIsDisplayed(collectionName);
    }

    async openCollection(collectionName: string): Promise<void> {
        await this.collectionPage.openCollection(collectionName);
    }

    async verifyThreePhotosAppearInCollection(collectionName: string): Promise<void> {
        await this.collectionPage.openCollection(collectionName);
        await this.collectionPage.verifyThreePhotosAppearInCollection();
    }

    async deleteCollectionByAPI(collectionId: string): Promise<void> {
        if (!collectionId) {
            return;
        }
        const response = await CollectionServices.deleteCollection(collectionId);
        const responseBody = await response.text();

        expect([200, 204], `Delete collection failed. Status: ${response.status()}. Body: ${responseBody}`).toContain(response.status());
    }
}