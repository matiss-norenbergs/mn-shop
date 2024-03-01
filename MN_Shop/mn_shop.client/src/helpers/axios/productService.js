import axios from "axios"

import { apiUrl, objects } from "@/helpers/constants"

const headers = {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
}

const getProductList = (cancelToken) => {
    return axios.get(`${apiUrl}${objects.product}`, {
        headers: headers
    }, {
        cancelToken: cancelToken
    })
}

const getProductData = (postParams, cancelToken) => {
    return axios.get(`${apiUrl}${objects.product}/${postParams?.id}`, {
        cancelToken: cancelToken
    })
}

const saveProduct = (postParams, cancelToken) => {
    return axios.post(`${apiUrl}${objects.product}`,
        postParams,
        {
            headers: headers
        },
        {
            cancelToken: cancelToken
        }
    )
}

const deleteProduct = (postParams, cancelToken) => {
    return axios.delete(`${apiUrl}${objects.product}/${postParams?.id}`, {
        cancelToken: cancelToken
    })
}

export {
    getProductList,
    getProductData,
    saveProduct,
    deleteProduct
}