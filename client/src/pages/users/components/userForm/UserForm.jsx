import { forwardRef, useCallback, useImperativeHandle } from "react"

import { saveUserData } from "helpers/axios/userService"

const UserForm = forwardRef(({}, ref) => {

    const handleUserSave = useCallback(() => {
        return new Promise((resolve, reject) => {
            const postParams = {
                name: "Ben",
                surname: "Swan",
                email: "test@test12.lv",
                password: "123"
            }

            saveUserData(postParams)
                .then(response => {
                    if (!!response && response.status === 201)
                        return resolve()

                    return reject()
                })
                .catch(() => {
                    reject()
                })
        })
    }, [])

    useImperativeHandle(ref, () => ({
        save: handleUserSave
    }), [handleUserSave])

    return (
        <div>
            User form
        </div>
    )
})

export default UserForm