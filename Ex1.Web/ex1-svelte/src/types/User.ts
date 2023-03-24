import type { Gender } from "./gender";

export interface User {
    id: number,
    FullName: string,
    Email: string,
    BornDate: Date,
    Gender: Gender,
    EducationMaxReached: string,
    Interests: string
}