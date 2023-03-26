import { Gender } from "./Gender";
import type { DateOnly } from "./DateOnly"
import { getNewDateOnly } from "./DateOnly"
interface User {
    id: number,
    fullName: string,
    email: string,
    bornDate: DateOnly,
    gender: Gender,
    educationMaxReached: string,
    interests: string
}

const getNewUser = (): User => {
    let user: User = {
        id: 0,
        fullName: "",
        bornDate: getNewDateOnly(),
        email: "",
        gender: Gender.NotSpecified,
        interests: "",
        educationMaxReached: "",
    };

    return user;
}

export {
    type User,
    getNewUser
}