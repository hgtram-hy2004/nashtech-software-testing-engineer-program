import { expect, Locator, Page } from '@playwright/test';
import { BARE_URL } from '../constant/url';

export class LoginPage {
  private readonly page: Page;
  private readonly userNameInput: Locator;
  private readonly passwordInput: Locator;
  private readonly loginBtn: Locator;
  private readonly loginErrorMessage: Locator;

  constructor(page: Page) {
    this.page = page;

    this.userNameInput = page.locator('input#username');
    this.passwordInput = page.locator('input#password');
    this.loginBtn = page.getByRole('button', { name: 'Login' });

    this.loginErrorMessage = page
      .locator(
        "form[name='loginForm'] div.alert-danger, " +
        "form[name='loginForm'] [role='alert'], " +
        "form[name='loginForm'] div[ng-messages]"
      )
      .filter({ hasText: /\S/ });
  }

  async openLoginPage(): Promise<void> {
    await this.page.goto(BARE_URL);
  }

  async enterUsername(username: string): Promise<void> {
    await this.userNameInput.fill(username);
  }

  async enterPassword(password: string): Promise<void> {
    await this.passwordInput.fill(password);
  }

  async clickLogin(): Promise<void> {
    await this.loginBtn.click();
  }

  async login(username: string, password: string): Promise<void> {
    await this.enterUsername(username);
    await this.enterPassword(password);
    await this.clickLogin();
  }

  async verifyLoginFailedMessageIsDisplayed(): Promise<void> {
    await expect(this.loginErrorMessage.first()).toBeVisible({
      timeout: 10000,
    });
  }

  async verifyLoginPageIsDisplayed(): Promise<void> {
    await expect(this.userNameInput).toBeVisible();
    await expect(this.passwordInput).toBeVisible();
    await expect(this.loginBtn).toBeVisible();
  }
}