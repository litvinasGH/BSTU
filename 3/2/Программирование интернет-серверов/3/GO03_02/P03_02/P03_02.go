package P03_02

import "fmt"

type Counters struct {
	get_count, post_count int
}

func (c *Counters) PlusGet() {
	c.get_count++
}

func (c *Counters) PlusPost() {
	c.post_count++
}

func (c *Counters) GenStr() string {
	return fmt.Sprintf("Get-request count = %d, Post-request count = %d", c.get_count, c.post_count)
}
