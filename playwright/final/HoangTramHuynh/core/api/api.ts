import { APIResponse } from '@playwright/test';
import { BrowserManagement } from '../browser/browser-management';

interface ApiOptions {
    params?: Record<string, string | number | boolean>;
    headers?: Record<string, string>;
    data?: Record<string, unknown> | string;
}

export class APIUtils {
    private static buildUrl(url: string, option?: ApiOptions): string {
        const query = new URLSearchParams(
            option?.params as Record<string, string>
        ).toString();

        return query ? `${url}?${query}` : url;
    }

    static async get(
        url: string,
        option?: ApiOptions
    ): Promise<APIResponse> {
        const fullUrl = this.buildUrl(url, option);

        return await BrowserManagement.getCurrentRequest().get(fullUrl, {
            headers: option?.headers,
        });
    }

    static async post(
        url: string,
        option?: ApiOptions
    ): Promise<APIResponse> {
        const fullUrl = this.buildUrl(url, option);

        return await BrowserManagement.getCurrentRequest().post(fullUrl, {
            headers: option?.headers,
            data: option?.data,
        });
    }

    static async put(
        url: string,
        option?: ApiOptions
    ): Promise<APIResponse> {
        const fullUrl = this.buildUrl(url, option);

        return await BrowserManagement.getCurrentRequest().put(fullUrl, {
            headers: option?.headers,
            data: option?.data,
        });
    }

    static async delete(
        url: string,
        option?: ApiOptions
    ): Promise<APIResponse> {
        const fullUrl = this.buildUrl(url, option);

        return await BrowserManagement.getCurrentRequest().delete(fullUrl, {
            headers: option?.headers,
            data: option?.data,
        });
    }
}