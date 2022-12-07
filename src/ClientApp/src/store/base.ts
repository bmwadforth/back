export interface IApiError {
    message: string;
    code: string;
}

export interface IApiResponse<T> {
    message: string;
    payload?: T;
    errors?: IApiError[];
}