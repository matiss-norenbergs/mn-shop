import PropTypes from "prop-types"
import { forwardRef, useCallback, useEffect, useImperativeHandle, useMemo } from "react"

import Form from "@/components/form"
import Input from "@/components/input"

import { getUserData, saveUserData } from "@/helpers/axios/userService"

//import styles from "./UserForm.module.css"

const propTypes = {
    objectId: PropTypes.number,
    setModalTitle: PropTypes.func
}
const defaultProps = {
    objectId: 0
}

const inputRules = [
    { required: true, message: "This field is required!" },
    { whitespace: true, message: "This field is required!" }
]

const UserForm = forwardRef(({
    objectId,
    setModalTitle
}, ref) => {

    const [form] = Form.useForm()

    const getUser = useCallback((userId) => {
        const postParams = {
            id: userId
        }

        getUserData(postParams)
            .then(response => {
                if (!!response && response.status === 200) {
                    const {
                        id,
                        name,
                        surname,
                        email,
                        password
                    } = response.data

                    if (id !== 0)
                        setModalTitle("Edit user")

                    form.setFieldsValue({
                        name,
                        surname,
                        email,
                        password,
                        password2: password
                    })
                }
            })
    }, [form, setModalTitle])

    const handleUserSave = useCallback(() => {
        return new Promise((resolve, reject) => {
            const postParams = {
                id: objectId
            }

            form.validateFields()
                .then(values => {
                    Object.keys(values).forEach(field => {
                        postParams[field] = values[field]
                    })

                    saveUserData(postParams)
                        .then(response => {
                            if (!!response && response.status === 200)
                                return resolve()
                        })
                        .catch(() => {
                            return reject()
                        })
                })
                .catch(_ => {
                    return reject()
                })
        })
    }, [form, objectId])

    useImperativeHandle(ref, () => ({
        save: handleUserSave
    }), [handleUserSave])

    useEffect(() => {
        getUser(objectId)
    }, [getUser, objectId])

    const validatePassword = useCallback(({ field }, value) => {
        return new Promise((resolve, reject) => {
            const otherPassword = form.getFieldValue(field === "password" ? "password2" : "password")
            if (value !== otherPassword)
                return reject("Passwords don't match!")

            return resolve()
        })
    }, [form])

    const passwordRules = useMemo(() => {
        return [
            ...inputRules,
            { validator: validatePassword }
        ]
    }, [validatePassword])

    return (
        <Form form={form}>
            <Form.Field
                required
                name="name"
                initialValue=""
                rules={inputRules}
                label="Name"
            >
                <Input />
            </Form.Field>
            <Form.Field
                required
                name="surname"
                initialValue=""
                rules={inputRules}
                label="Surname"
            >
                <Input />
            </Form.Field>
            <Form.Field
                required
                name="email"
                initialValue=""
                rules={inputRules}
                label="Email"
            >
                <Input />
            </Form.Field>
            <Form.Field
                required
                name="password"
                initialValue=""
                rules={passwordRules}
                label="Password"
            >
                <Input password />
            </Form.Field>
            <Form.Field
                required
                name="password2"
                initialValue=""
                rules={passwordRules}
                label="Password (repeat)"
            >
                <Input password />
            </Form.Field>
        </Form>
    )
})
UserForm.propTypes = propTypes
UserForm.defaultProps = defaultProps

UserForm.displayName = 'UserForm'

export default UserForm