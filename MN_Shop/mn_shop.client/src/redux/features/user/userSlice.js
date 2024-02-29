import { createSlice } from "@reduxjs/toolkit"

export const userSlice = createSlice({
    name: "user",
    initialState: {},
    reducers: {
        setUser: (state, action) => {
            state = Object.assign(state, action.payload)
        },
        removeUser: (state) => {
            state = {}
        }
    }
})

export const {
    setUser,
    removeUser
} = userSlice.actions

export default userSlice.reducer