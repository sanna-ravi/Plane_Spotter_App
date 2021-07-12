import { BaseModel } from "./base-model";

export class PlaneSpotterModel extends BaseModel {
    public make?: string;    
    public model?: number;
    public registration?: string;
    public location?: number;
    public dateandTime?: string;
    public spotterImage?: File;
    public spotterImageUrl?: string;
}
