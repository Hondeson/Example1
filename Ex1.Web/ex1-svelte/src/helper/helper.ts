const isNullOrWhitespace = (input: string): boolean => {
    return !input || !input.trim();
};

export {
    isNullOrWhitespace
}