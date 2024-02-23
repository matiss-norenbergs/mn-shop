import { forwardRef, useCallback, useImperativeHandle } from "react"

const UserForm = forwardRef(({}, ref) => {

    const handleUserSave = useCallback(() => {
        return new Promise((resolve, reject) => {
            reject()
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