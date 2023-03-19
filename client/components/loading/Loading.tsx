import styles from './Loading.module.scss'

interface LoadingProps {
  loading: boolean
}

const Loading = (props: LoadingProps) => {
  const { loading } = props

  return (
    <div className={styles.load}>{loading && <div className={styles.loader} />}</div>
  )
}

export default Loading
