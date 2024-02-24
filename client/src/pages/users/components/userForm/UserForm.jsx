import PropTypes from "prop-types"
import { forwardRef, useCallback, useEffect, useImperativeHandle } from "react"
import Form, { Field } from "rc-field-form"

import Input from "components/input"

import { getUserData, saveUserData } from "helpers/axios/userService"

//import styles from "./UserForm.module.css"

const propTypes = {
    objectId: PropTypes.number,
    setModalTitle: PropTypes.func
}
const defaultProps = {
    objectId: 0
}

const inputRules = [
    { required: true, message: "This field is required" },
    { whitespace: true, message: "This field is required" }
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
                    const data = response.data

                    if (data.id !== 0)
                        setModalTitle("Edit user")

                    form.setFieldsValue(data)
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

    return (
        <Form form={form}>
            <Field
                name="name"
                initialValue={""}
                rules={inputRules}
            >
                <Input placeholder="Name..." />
            </Field>
            <Field
                name="surname"
                initialValue={""}
                rules={inputRules}
            >
                <Input placeholder="Surname..." />
            </Field>
            <Field
                name="email"
                initialValue={""}
                rules={inputRules}
            >
                <Input placeholder="Email..." />
            </Field>
            <Field
                name="password"
                initialValue={""}
            >
                <Input placeholder="Password..." />
            </Field>
        </Form>
    )
})
UserForm.propTypes = propTypes
UserForm.defaultProps = defaultProps

export default UserForm