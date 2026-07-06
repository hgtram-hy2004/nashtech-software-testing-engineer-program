import { expect, Locator } from "@playwright/test";
import { BrowserManagement } from "../browser/browser-management";

export class Element {
    locator: string;

    constructor(locator: string) {
        this.locator = locator;
    }

    getElement() {
        return BrowserManagement.page.locator(this.locator);
    }
    async clickOnElement(): Promise<void> {
        await this.getElement().click();
    }
    async shouldBeVisible(): Promise<void> {
        await expect(this.getElement()).toBeVisible();
    }
    async enterText(text: string): Promise<void> {
        await this.getElement().fill(text);
    }
    async getText(): Promise<string> {
        return (await this.getElement().textContent()) ?? '';
    }
    async clearText(): Promise<void> {
        await this.waitForElementVisible();
        await this.getElement().clear();
    }
    async doesElementContainText(expectedText: string): Promise<void> {
        await expect(BrowserManagement.page.locator(this.locator)).toContainText(expectedText);
    }
    async waitForElementDisplayed(timeout: number = 10000): Promise<void> {
        await this.getElement().waitFor({
            state: 'visible',
            timeout,
        });
    }
    async shouldBeVisibleIfExists(): Promise<void> {
        const count = await this.getElement().count();

        if (count > 0) {
            await expect(this.getElement().first()).toBeVisible();
        }
    }
    async hoverOnElement(): Promise<void> {
        await this.getElement().hover();
    }
    async waitForElementVisible(timeout: number = 10000): Promise<void> {
        await this.getElement().waitFor({
            state: 'visible',
            timeout,
        });
    }
}