import { useCallback, useEffect, useRef, useState } from 'react'
import axios from 'axios'

import Layout from '@/components/layout'
import Heading from '@/components/heading'
import Table from "@/components/table"
import Button from "@/components/button"
import ModalComponent from "@/components/modalComponent"

import ProductForm from './components/productForm'

import { deleteProduct, getProductList } from '@/helpers/axios/productService'

import styles from "./Products.module.css"

const columns = [
    {
        field: "select",
        name: "",
        width: 30
    },
    {
        field: "name",
        name: "Name",
        width: 200
    },
    {
        field: "priceString",
        name: "Price",
        width: 150,
        align: "right"
    },
    {
        field: "description",
        name: "Description",
        width: 300
    },
]

const Products = () => {
    const [data, setData] = useState([])
    const [selectedRows, setSelectedRows] = useState([])
    const [isDataLoading, setIsDataLoading] = useState(false)

    const productFormModalElementRef = useRef(null)
    const axiosCancelToken = useRef(null)

    const isRowSelected = selectedRows.length === 1

    const getProducts = useCallback(() => {
        setIsDataLoading(true)

        getProductList(axiosCancelToken.current.token)
            .then(response => {
                if (!!response && response.status === 200)
                    setData(response.data)
            })
            .catch(() => {
                setData([])
            })
            .finally(() => {
                setIsDataLoading(false)
            })
    }, [])

    const handleCreateClick = useCallback(() => {
        productFormModalElementRef.current?.open()
    }, [])

    const handleEditClick = useCallback(() => {
        if (!isRowSelected)
            return

        productFormModalElementRef.current?.open({ objectId: selectedRows[0].id })
    }, [selectedRows, isRowSelected])

    const handleDeleteClick = useCallback(() => {
        if (!isRowSelected)
            return

        setIsDataLoading(true)

        const postParams = {
            id: selectedRows[0].id
        }

        deleteProduct(postParams, axiosCancelToken.current.token)
            .then(response => {
                if (!!response && response.status === 204) {
                    getProducts()
                }
            })
            .catch(() => {
                setIsDataLoading(false)
            })
    }, [getProducts, isRowSelected, selectedRows])

    useEffect(() => {
        axiosCancelToken.current = axios.CancelToken.source()

        getProducts()
    }, [getProducts])

    useEffect(() => {
        return () => axiosCancelToken.current?.cancel
    }, [])

    const toolbar = (
        <>
            <Button.Group disabled={isDataLoading}>
                <Button
                    type="primary"
                    onClick={handleCreateClick}
                    faIcon="plus"
                >
                    {"Create"}
                </Button>
                <Button
                    onClick={handleEditClick}
                    disabled={!isRowSelected}
                    faIcon="edit"
                >
                    {"Edit"}
                </Button>
                <Button
                    onClick={handleDeleteClick}
                    disabled={!isRowSelected}
                    faIcon="trash-alt"
                >
                    {"Delete"}
                </Button>
            </Button.Group>
            <Button.Spacer />
            <Button
                onClick={getProducts}
                disabled={isDataLoading}
                faIcon="sync"
            >
                {"Refresh"}
            </Button>
        </>
    )

    return (
        <Layout>
            <Heading
                level={3}
                center
            >
                {"Products"}
            </Heading>
            <Table
                toolbar={toolbar}
                columns={columns}
                data={data}
                getSelectedRows={setSelectedRows}
            />
            <ModalComponent
                ref={productFormModalElementRef}
                component={<ProductForm />}
                title="Create product"
                confirmText="Save"
                onConfirm={getProducts}
                width={500}
            />
        </Layout>
    )
}

export default Products