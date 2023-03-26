CREATE TABLE IF NOT EXISTS public.users
(
    id BIGSERIAL PRIMARY KEY,
    full_name TEXT NOT NULL,
    email TEXT NOT NULL,
    born_date date NOT NULL,
    gender INT NOT NULL,
    education_max_reached INT NOT NULL,
    interests TEXT
)