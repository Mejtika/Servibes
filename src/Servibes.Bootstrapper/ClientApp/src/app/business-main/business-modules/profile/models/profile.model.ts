export interface IProfile {
    companyId: string;
    companyName: string;
    phoneNumber: string;
    category: string;
    description: string;
    coverPhoto: string;
    address: IAddress;
}

export interface IAddress {
    city: string;
    zipcode: string;
    street: string;
    flatNumber: string;
    streetNumber: string;
}