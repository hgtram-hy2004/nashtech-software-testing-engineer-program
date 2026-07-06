import { Locator, Page } from '@playwright/test';

export class HomePage {
  private readonly page: Page;
  private readonly avatar: Locator;

  constructor(page: Page) {
    this.page = page;
    this.avatar = page.locator('img#ava');
  }

  private accountLoginInformation(username: string): Locator {
    return this.page.locator(
      `//img[@id='ava']/ancestor::a[contains(normalize-space(.),'${username}')]`
    );
  }

    async verifyHomePageIsDisplayed(): Promise<void> {
    await this.avatar.waitFor({ state: 'visible', timeout: 15000 });
    }

    async verifyLoginWithCorrectAccount(username: string): Promise<void> {
    await this.accountLoginInformation(username).waitFor({
        state: 'visible',
        timeout: 15000,
    });
    }

}