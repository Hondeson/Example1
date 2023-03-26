interface DateOnly {
    year: number,
    month: number,
    day: number
}

const getNewDateOnly = (): DateOnly => {
    let bornDate = {
        year: 0,
        month: 0,
        day: 0
    };

    return bornDate;
}

const dateOnlyHasValue = (dateOnly: DateOnly): boolean => {
    if (dateOnly.year === 0)
        return false;

    return true;
}

export {
    type DateOnly,
    getNewDateOnly,
    dateOnlyHasValue
}