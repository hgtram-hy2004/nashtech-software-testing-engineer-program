export interface PhotoResponse {
    id: string;
    description?: string;
    alt_description?: string;

    urls: {
        raw?: string;
        full?: string;
        regular?: string;
        small?: string;
        thumb?: string;
    };

    user: {
        id: string;
        username: string;
        name: string;
    };

    // Used to skip Unsplash+ photos
    plus?: boolean;
    premium?: boolean;
}