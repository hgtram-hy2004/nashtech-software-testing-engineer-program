import { APIResponse } from '@playwright/test';
import { API_ENDPOINT, ACCESS_TOKEN } from '../constant/url';
import { APIUtils } from '../core/api/api';

export class CollectionServices {
    private static getHeaders(): Record<string, string> {
        if (!ACCESS_TOKEN) {
            throw new Error('Access token is missing.');
        }
        return {
            Authorization: `Bearer ${ACCESS_TOKEN}`,
            'Content-Type': 'application/json',
        };
    }

    static async createCollection(collectionName: string): Promise<APIResponse> {
        return await APIUtils.post(`${API_ENDPOINT}/collections`, {
            headers: this.getHeaders(),
            data: {
                title: collectionName,
                private: false,
            },
        });
    }

    static async addPhotoToCollection(collectionId: string, photoId: string): Promise<APIResponse> {
        return await APIUtils.post(
            `${API_ENDPOINT}/collections/${collectionId}/add`,
            {
                headers: this.getHeaders(),
                data: {
                    photo_id: photoId,
                },
            }
        );
    }

    static async deleteCollection(collectionId: string): Promise<APIResponse> {
        return await APIUtils.delete(
            `${API_ENDPOINT}/collections/${collectionId}`,
            {
                headers: this.getHeaders(),
            }
        );
    }
}