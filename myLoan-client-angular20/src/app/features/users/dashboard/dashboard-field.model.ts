export interface DashboardResponseDto{
    // email: string;
    // lastName: string;
    // firstName: string;
    // userId: string;

    userId: number
    firstName: string;
    lastName: string;
    email: string;  
    phone: string;
    dateOfBirth: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    stateProvince: string;
    postalCode: string;
    country: string;
    createdAt: string;
    updatedAt: string;
    status: number;
}