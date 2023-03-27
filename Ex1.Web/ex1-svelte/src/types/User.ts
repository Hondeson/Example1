import { Gender } from "./Gender";
import type { DateOnly } from "./DateOnly"
import { getNewDateOnly } from "./DateOnly"
import { Education } from "./Education";
interface User {
    id: number,
    fullName: string,
    email: string,
    bornDate: DateOnly,
    gender: Gender,
    educationMaxReached: Education,
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
        educationMaxReached: Education.NotSpecified,
    };

    return user;
}

export {
    type User,
    getNewUser
}