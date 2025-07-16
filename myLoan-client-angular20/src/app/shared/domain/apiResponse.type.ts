export interface apiResponse<T>{
    success: boolean,
    data? : T,
    message: string;
}