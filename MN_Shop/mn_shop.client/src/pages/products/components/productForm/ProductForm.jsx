import PropTypes from "prop-types"
import { forwardRef, useCallback, useEffect, useImperativeHandle, useRef } from "react"
import axios from "axios"

import Form from "@/components/form"
import Input from "@/components/input"
import TextArea from "@/components/textArea"
import InputNumber from "@/components/inputNumber"

import { saveProduct, getProductData } from "@/helpers/axios/productService"

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

const ProductForm = forwardRef(({
    objectId,
    setModalTitle
}, ref) => {
    const [form] = Form.useForm()

    const axiosCancelToken = useRef(null)

    const handleGetProduct = useCallback((productId) => {
        const postParams = {
            id: productId
        }

        getProductData(postParams, axiosCancelToken.current?.token)
            .then(response => {
                if (!!response && response.status === 200) {
                    const {
                        id,
                        name,
                        price,
                        description
                    } = response.data

                    if (id !== 0)
                        setModalTitle("Edit product")

                    form.setFieldsValue({
                        name,
                        price,
                        description
                    })
                }
            })
    }, [form, setModalTitle])

    const handleProductSave = useCallback(() => {
        return new Promise((resolve, reject) => {
            const postParams = {
                id: objectId
            }

            form.validateFields()
                .then(values => {
                    Object.keys(values).forEach(field => {
                        postParams[field] = values[field]
                    })

                    saveProduct(postParams, axiosCancelToken.current?.token)
                        .then(response => {
                            if (!!response && response.status === 200)
                                return resolve()
                        })
                        .catch(() => {
                            return reject()
                        })
                })
                .catch(() => {
                    return reject()
                })
        })
    }, [form, objectId])

    useImperativeHandle(ref, () => ({
        save: handleProductSave
    }), [handleProductSave])

    useEffect(() => {
        handleGetProduct(objectId)
    }, [handleGetProduct, objectId])

    useEffect(() => {
        axiosCancelToken.current = axios.CancelToken.source()

        return () => axiosCancelToken.current?.cancel
    }, [])

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
                name="price"
                initialValue={0}
                //rules={inputRules}
                label="Price"
            >
                <InputNumber
                    min={0}
                />
            </Form.Field>
            <Form.Field
                name="description"
                initialValue=""
                label="Description"
            >
                <TextArea rows={5} />
            </Form.Field>
        </Form>
    )
})
ProductForm.propTypes = propTypes
ProductForm.defaultProps = defaultProps

ProductForm.displayName = "ProductForm"

export default ProductForm