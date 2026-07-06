import { defineConfig, devices } from '@playwright/test';
import { BARE_URL } from './constant/url';

export default defineConfig({
  testDir: './tests',
  testMatch: '**/*.spec.ts',
  fullyParallel: false,
  timeout: 30000,
  expect: {
    timeout: 10000,
  },
  reporter: [
    ['list'],
    ['html', { open: 'never' }],
  ],

  use: {
    baseURL: BARE_URL,

    headless: false,

    viewport: {
      width: 1366,
      height: 768,
    },

    screenshot: 'only-on-failure',
    video: 'retain-on-failure',
    trace: 'retain-on-failure',
  },

  projects: [
    {
      name: 'chromium',
      use: {
        ...devices['Desktop Chrome'],
      },
    },
  ],
});