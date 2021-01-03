import { StarTemplateContext } from "@ng-bootstrap/ng-bootstrap/rating/rating";
import { DayOfWeek } from "src/app/shared/interfaces/company";

export interface IProfile {
    companyId: string;
    companyName: string;
    phoneNumber: string;
    category: string;
    description: string;
    coverPhotoId: string;
    address: IAddress;
}

export interface IAddress {
    city: string;
    zipcode: string;
    street: string;
    flatNumber: string;
    streetNumber: string;
}

export interface IHoursRange {
    dayOfWeek: number;
    isAvailable: boolean;
    start: string;
    end: string;
}

export interface IImage {
    imageId: string;
    fileType: string;
    data: File;
}